using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreTuto.ConsoleApp
{
    internal class BaseEntity: INotifyPropertyChanged
    {
        private bool deleted;

        public int Id { get; set; }


        public bool Deleted
        {
            get
            {
                return this.deleted;
            }

            set
            {
                if (value != this.deleted)
                {
                    this.deleted = value;
                    NotifyPropertyChanged();
                }
            }
        }
 
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
