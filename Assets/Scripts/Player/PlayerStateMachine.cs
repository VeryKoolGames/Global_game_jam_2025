using System.Threading.Tasks;

namespace Player
{
    public class PlayerStateMachine
    {
        public PlayerState _currentState { get; private set; }
    
        public void Initialize(PlayerState initialState)
        {
            _currentState = initialState;
            _currentState.Enter();
        }
    
        public async void ChangeState(PlayerState newState)
        {
            if (newState == _currentState)
            {
                return;
            }
            
            if (_currentState != null)
            {
                _currentState.Exit();
            }
        
            _currentState = newState;
        
            if (_currentState != null)
            {
                 await _currentState.Enter();
            }
        }
    
        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }
    }
}