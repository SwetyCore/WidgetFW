using CommunityToolkit.Mvvm.ComponentModel;
using DGP.Genshin.GamebarWidget.Model;

namespace DefaultStyle.VM
{
    internal class GenshinHelper : ObservableObject
    {
        private RoleAndNote _roleAndNote;

        public RoleAndNote RoleAndNote
        {
            get { return _roleAndNote; }
            set { SetProperty(ref _roleAndNote, value); }
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }


    }
}
