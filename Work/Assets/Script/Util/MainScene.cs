using UnityEngine;

public class MainScene : Singleton<MainScene>
{
    FSM _fsm = new FSM();

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void Init()
    {
        _fsm.RemoveAllState();

        _fsm.SetGbjAndTrans(gameObject, transform);

        //

        _fsm.InitStates();
    }

    private void Start()
    {
        //_fsm.ChangeState();
    }
}
