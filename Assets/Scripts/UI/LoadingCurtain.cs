using System.Collections;
using UnityEngine;

namespace UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.alpha = 1f;
        }

        public void Hide() =>
            StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            while (curtain.alpha > 0)
            {
                curtain.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }
            
            gameObject.SetActive(false);
        }
        
    }
}