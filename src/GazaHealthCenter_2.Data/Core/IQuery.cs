namespace GazaHealthCenter_2.Data;

public interface IQuery<TModel> : IQueryable<TModel>
{
    IQuery<TModel> Where(Expression<Func<TModel, Boolean>> predicate);

    IQueryable<TView> To<TView>();
}
