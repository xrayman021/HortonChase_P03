using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCommand : ICommand
{
    IBuffable _buffableToken;

    public BuffCommand(IBuffable buffableToken)
    {
        _buffableToken = buffableToken;
    }

    public void Execute()
    {
        _token.Buff();
    }

    public void UndoExecute()
    {
        _token.Debuff();
    }

}
