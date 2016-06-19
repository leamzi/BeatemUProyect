public interface iPlayableState {

    void Enter(PlayableEntity playable_entity);
    iPlayableState HandleInput(PlayableEntity playable_entity, PlayableActions actions);
}
