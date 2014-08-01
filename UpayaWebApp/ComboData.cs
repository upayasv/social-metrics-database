using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    public interface IComboData
    {
        string Id { get; }
        string Title { get; }
    }

    public class ComboData : IComboData
    {
        public string mId, mTitle;

        public string Id { get { return mId; } }
        public string Title { get { return mTitle; } }

        public ComboData(string id, string title)
        {
            mId = id;
            mTitle = title;
        }
    }
}