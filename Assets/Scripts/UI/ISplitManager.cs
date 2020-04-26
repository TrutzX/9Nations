using UI.Show;

namespace UI
{
    public interface ISplitManager
    { 
        void Add(SplitElement ele, bool first = false);
        
        int Count();
        
        void Close();
    }
}