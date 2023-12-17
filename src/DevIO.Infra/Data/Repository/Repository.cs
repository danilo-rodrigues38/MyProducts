using DevIO.Business.Core.Data;
using DevIO.Business.Core.Models;
using DevIO.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Repository
{
    // O repositório será uma classe abstrata, não poderá ser instanciada sozinha, terá que ser herdada.
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        // Cria um acesso ao contexto definido como Db, será "protected", pois só quem herda dela vai enxergar.
        protected readonly MyDbContext Db;

        // DbSet é um acesso rápido a minha entidade, enquanto o Db é o contexto em geral.
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MyDbContext db)
        {
            Db = db;

            // DbSet é um atalho a entidade TEntity
            DbSet = Db.Set<TEntity>();
        }

        // O método será virtual e ascíncronos, o método recebeu um id e retorna uma consulta,
        // retornado uma entidade através da chave primária.
        public virtual async Task<TEntity> Obter ( Guid id )
        {
            return await DbSet.FindAsync ( id );
        }

        // Faz o mesmo que o método "Obter", mas retorna uma lista com todas as entidades do banco de dados.
        public virtual async Task<List<TEntity>> ObterTodos ( )
        {
            return await DbSet.ToListAsync ( );
        }

        // O método "Buscar" é um método que vai implementar uma expressão, com buscas em determinados critérios,
        // não há a necessidade de ser virtual, pois não será preciso sobrescrever.
        public async Task<IEnumerable<TEntity>> Buscar ( Expression<Func<TEntity, bool>> predicate )
        {
            // O "AsNoTraking", desabilita o Traker da consulta para dar perfornance no banco de dados, pois
            // o EntityFramework não precisa acompanhar os dados da entidade para ver se ela mudou para ver
            // se precisa persistir novamente no banco de dados.
            // O "predicate" é onde o método recebe a expressão para a pesquisa no banco de dados.
            return await DbSet.AsNoTracking ( ).Where ( predicate ).ToListAsync ( );
        }

        // Este método adiciona uma entidade ao banco de dados.
        public virtual async Task Adicionar ( TEntity entity )
        {
            DbSet.Add ( entity );
            await SaveChanges ( );
        }

        // Este método vai modificar o estado da entidade, ou seja, ele vai receber uma entidade "entity",
        // e vai verificar o estado da entidade e salvar as modificações no banco de dados, ele vai entender
        // que não é para adicionar e sim para somente modificar.
        public virtual async Task Atualizar ( TEntity entity )
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges ( );
        }

        // Este método vai remover uma entidade, ou seja, ele vai receber uma entidade por "id",
        // vai criar uma nova entidade pelo id recebido, vai verificar o estado da entidade e irá
        // remove-la do banco de dados.
        // Para dar um "new" em TEntity, você precisa definir no escopo/assinatura da classe que é
        // possível iniciar uma nova classe/entidade, incluindo depois de "Entity" uma vírgula e o "new()".
        public virtual async Task Remover ( Guid id )
        {
            Db.Entry ( new TEntity { Id = id } ).State = EntityState.Deleted;
            await SaveChanges ( );
        }

        // A função do "SaveChanges" é de salvar no banco de dados e retornar um valor inteiro, 1 ou 0,
        // sendo 1 que diz que foi salvo uma linha no banco de dados e o valor 0, diz que não salvou nada
        // no banco de dados.
        public async Task<int> SaveChanges ( )
        {
            return await Db.SaveChangesAsync ( );
        }

        // Este método só será chamado se houver uma instância definida, que é a função do "?".
        public void Dispose ( )
        {
            Db?.Dispose ( );
        }
    }
}
