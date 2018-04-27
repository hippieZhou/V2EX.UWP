using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2EX.Models
{
    public class ControlInfoDataGroup
    {
        public string Title { get; private set; }
        public ObservableCollection<Node> Items { get; private set; }
        public ControlInfoDataGroup(string title, IEnumerable<Node> nodes)
        {
            this.Title = title;
            this.Items = new ObservableCollection<Node>(nodes);
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
