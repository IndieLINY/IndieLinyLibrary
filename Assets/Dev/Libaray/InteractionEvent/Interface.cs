using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base interaction
 * --
 */
public interface IBaseBehaviour
{
    public InteractionController Interaction { get; }
}
public interface IObjectBehaviour : IBaseBehaviour
{
    
}
public interface IActorBehaviour : IBaseBehaviour
{
}
