using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BrowserUserControl.Model
{
    public class BrowsingHistory
    {
        public Uri Url { get; set; }
        public DateTime BrowsingTime { get; set; } = DateTime.Now;
    }

    public class ListBrowsingHistory
    {
        public const int MAX_HISTORY_SIZE = 25;
        public int CurrentPointer { get; set; }
        public List<BrowsingHistory> _list_history = new List<BrowsingHistory> ();

        public int Count { get {  return _list_history.Count; } }
        public Uri CurrentURL { get {
                if (0 <= CurrentPointer && CurrentPointer < Count)
                    return _list_history[CurrentPointer].Url;
                else
                    return null;
            }
        }
        public BrowsingHistory this[int i]
        {
            get { return _list_history[i]; }
            set { _list_history[i] = value; }
        }

        private void RemoveRange(int index, int count)
        {
            _list_history.RemoveRange(index, count);
        }

        private void Add(BrowsingHistory browsingHistory)
        {
            if (_list_history.Count < MAX_HISTORY_SIZE)
            {
                _list_history.Add(browsingHistory);
            }
            else
            {
                _list_history.RemoveAt(0);
                CurrentPointer--;
                _list_history.Add(browsingHistory);
            }
        }

        public void Navigate(BrowsingHistory history)
        {
            if (CurrentPointer < this.Count - 1 && history.Url.AbsoluteUri != "https://www.3dfindit.com/")
                this.RemoveRange(CurrentPointer + 1, this.Count - CurrentPointer - 1);
            CurrentPointer = this.Count;
            this.Add(history);
        }

        public BrowsingHistory GoBack()
        {
            if (CurrentPointer <= 0)
            {
                return new BrowsingHistory();
            }
            else
            {
                CurrentPointer--;
                return this[CurrentPointer];
            }
        }

        public BrowsingHistory PeekBack()
        {
            if (CurrentPointer <= 0)
            {
                return new BrowsingHistory();
            }
            else
            {
                return this[CurrentPointer - 1];
            }
        }

        public BrowsingHistory GoForward()
        {
            if (CurrentPointer >= this.Count - 1)
            {
                return new BrowsingHistory();
            }
            else
            {
                CurrentPointer++;
                return this[CurrentPointer];
            }
        }

        public BrowsingHistory PeekForward()
        {
            if (CurrentPointer >= this.Count - 1)
            {
                return new BrowsingHistory();
            }
            else
            {
                return this[CurrentPointer + 1];
            }
        }
    }
}
