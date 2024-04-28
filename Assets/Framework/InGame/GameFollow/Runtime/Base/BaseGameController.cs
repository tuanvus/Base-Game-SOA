using UnityEngine;
using LTA.DesignPattern;



namespace LTA.GameFollow
{
    public class GameObserverKey
    {
        public const string QuitGame = "QuitGame";
        public const string Lose = "Lose";
        public const string Win = "Win";
        public const string Draw = "Draw";
        public const string OnPause = "OnPause";
        public const string OnContinue = "OnContinue";
    }
    public abstract class BaseGameController : MonoBehaviour
    {
        public static bool isLose = false;
        public static bool isDraw = false;
        public static bool isWin = false;

        protected virtual void Awake()
        {
            Time.timeScale = 1;
            Observer.Instance.AddObserver(GameObserverKey.QuitGame, OnQuit);
            Observer.Instance.AddObserver(GameObserverKey.Win, OnWin);
            Observer.Instance.AddObserver(GameObserverKey.Draw, OnDraw);
            Observer.Instance.AddObserver(GameObserverKey.Lose, OnLose);
            Observer.Instance.AddObserver(GameObserverKey.OnPause, OnPause);
            Observer.Instance.AddObserver(GameObserverKey.OnContinue, OnCountinue);
        }

        static bool isPause = false;

        public static bool IsPause
        {
            get
            {
                return isPause;
            }
        }

        public static bool IsLose
        {
            get
            {
                return isLose;
            }
        }

        public static bool IsDraw => isDraw;

        void OnPause(object data)
        {
            isPause = true;
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0;
        }

        void OnCountinue(object data)
        {
            OnContinue();
            isPause = false;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }

        void OnLose(object data)
        {

            ResetGame();
            if (isDraw || isWin || isLose) return;
            isLose = true;     
            OnLose();
        }
        
        void OnDraw(object data)
        {
            
            ResetGame();
            if (isDraw || isWin || isLose) return;
            isDraw = true;
            OnDraw();
        }
        void OnWin(object data)
        {
            ResetGame();
            if (isDraw || isWin || isLose) return;
            isWin = true;
            OnWin();
            
        }

        protected void OnQuit(object data)
        {
            ResetGame();
            OnQuit();
        }

        protected abstract void OnWin();
        protected abstract void OnDraw();
        protected abstract void OnLose();
        protected abstract void OnQuit();
        protected virtual void ResetGame()
        {
            isPause = false;
        }

        protected abstract void OnContinue();
        

        protected virtual void OnDestroy()
        {
            isLose = false;//popup show win
            isDraw = false;//popup show win
            isWin = false;//popup show win
            Observer.Instance.RemoveObserver(GameObserverKey.QuitGame, OnQuit);
            Observer.Instance.RemoveObserver(GameObserverKey.Win, OnWin);
            Observer.Instance.RemoveObserver(GameObserverKey.Draw, OnDraw);
            Observer.Instance.RemoveObserver(GameObserverKey.Lose, OnLose);
            Observer.Instance.RemoveObserver(GameObserverKey.OnPause, OnPause);
            Observer.Instance.RemoveObserver(GameObserverKey.OnContinue, OnCountinue);
        }
        
        
    }
}
