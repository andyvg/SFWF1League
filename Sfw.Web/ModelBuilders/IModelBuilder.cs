using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sfw.Web.ModelBuilders
{
    public interface IModelBuilder<TViewModel, TModel>
    {
        TModel CreateModel(TViewModel viewModel);
        TModel UpdateModel(TViewModel viewModel);
        TViewModel CreateViewModel(TModel model);
    }
}
