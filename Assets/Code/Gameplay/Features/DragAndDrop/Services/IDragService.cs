using Code.Gameplay.Features.DragAndDrop.Behaviours;

namespace Code.Gameplay.Features.DragAndDrop.Services
{
    public interface IDragService
    {
        void StartDragging(DraggableItemBehaviour item);
        void StopDragging();
    }
}