using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Web.ModelBuilders
{
    public interface IListModelBuilder<TListViewModel, TViewModel, TModel> : IModelBuilder<TViewModel, TModel>
    {
        TListViewModel BuildListViewModel(IEnumerable<TModel> models);
    }
}