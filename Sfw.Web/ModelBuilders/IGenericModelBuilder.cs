using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sfw.Web.ModelBuilders
{
    public interface IGenericModelBuilder
    {
        TModel CreateModel<TViewModel, TModel>(TViewModel viewModel) where TModel : new();
        TViewModel CreateViewModel<TViewModel, TModel>(TModel model) where TViewModel : new();
        List<TDest> CreateList<TSource, TDest>(List<TSource> modelList) where TDest : new();
    }
}
