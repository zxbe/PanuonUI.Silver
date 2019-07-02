﻿
using System.Windows.Media;

namespace Panuon.UI.Silver.Browser.Models
{
    public class TimelineItemModel : PropertyChangedBase
    {
        #region Constructor
        public TimelineItemModel()
        {
        }

        public TimelineItemModel(string header,string content)
        {
            Header = header;
            Content = content;
        }
        #endregion

        #region Property
        public string Header
        {
            get { return _header; }
            set { _header = value; NotifyPropertyChanged("Header"); }
        }
        private string _header;

        public string Content
        {
            get { return _content; }
            set { _content = value; NotifyPropertyChanged("Content"); }
        }
        private string _content;
        #endregion
    }
}