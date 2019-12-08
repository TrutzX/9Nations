using System.Collections;

namespace Loading
{
    public interface ILoadScreen
    {
        IEnumerator ShowMessage(string text);

        IEnumerator ShowSubMessage(string text);
        
        void FinishLoading();
    }
}
