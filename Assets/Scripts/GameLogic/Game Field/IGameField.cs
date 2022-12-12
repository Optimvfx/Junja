using Game.Core;
using UnityEngine;

namespace Game.GameLogic
{
    public interface IGameField : ITickHandler<int>, ITickHandler
    {
        bool TryMove(HexVectorInt moveStartPosition, HexDirection.Direction moveDirection);
    }
}