using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Withywoods.AspNetCoreApiSample.Utils
{
    /// <summary>
    /// A tracker that allows to keep track of any changed properties
    /// </summary>
    public class PropertiesChangedTracker : INotifyPropertyChanged
    {
        /// <summary>
        /// List of properties which changed since last call to AcceptChanges or object construction
        /// </summary>
        public IList<string> ChangedProperties { get; private set; }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertiesChangedTracker class constructor
        /// </summary>
        protected PropertiesChangedTracker()
        {
            ChangedProperties = new List<string>();
        }

        /// <summary>
        /// Reinit change status for all object's properties
        /// </summary>
        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        /// <summary>
        /// Method to call from property setter to notify that a property value changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;

            if (!ChangedProperties.Contains(propertyName))
            {
                ChangedProperties.Add(propertyName);
            }

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
