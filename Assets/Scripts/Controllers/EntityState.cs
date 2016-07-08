public interface iEntityState
{
    void Enter(CharacterEntity playable_entity);
    iPlayableState HandleInput(CharacterEntity playable_entity, PlayableActions actions);
}