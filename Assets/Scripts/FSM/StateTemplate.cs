/// <summary>
/// 状态拥有者
/// </summary>
public class StateTemplate<T> : StateBase
{

    public T Owner;   //拥有者(范型)

    public StateTemplate(PlayerState id, T o) : base(id)
    {
        Owner = o;
    }
}