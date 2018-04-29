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
        public Char Character { get; private set; }
        public ObservableCollection<Node> Items { get; private set; }
        public ControlInfoDataGroup(char character, IEnumerable<Node> nodes)
        {
            this.Character = character;
            this.Items = new ObservableCollection<Node>(nodes);
        }

        public override string ToString()
        {
            return this.Character.ToString();
        }
    }
}
