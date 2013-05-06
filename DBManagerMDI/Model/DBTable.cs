using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//add using namespace
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DBManagerMDI.Model
{

    public class DBTable : INotifyPropertyChanged
    {

        private System.String _name;
        private ObservableCollection<DBTable> _subordinates;
        public event PropertyChangedEventHandler PropertyChanged;


        public System.String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public ObservableCollection<DBTable> Subordinates
        {
            get
            {
                if (null == _subordinates )
                {
                  _subordinates = new ObservableCollection<DBTable>();
                }
                return _subordinates;
            }
        }


        protected void RaisePropertyChanged(System.String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }//RaisePropertyChanged
    }
}
