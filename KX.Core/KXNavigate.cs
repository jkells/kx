using System;

namespace KX.Core
{
    public abstract class KXNavigate
    {
        public abstract void NavigateTo(Type viewModel);
        public abstract void NavigateBack();
    }
}
