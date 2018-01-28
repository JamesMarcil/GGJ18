using UnityEngine;

[CreateAssetMenu(fileName="COLOR_COMMAND", menuName="Create Color Command")]
public class ColorCommandAsset : BaseCommandAsset
{
    [SerializeField]
    private EntityColor m_color;

    [SerializeField]
    private BaseCommandAsset m_command;

    override public BaseCommand Make(GameObject target)
    {
        return ColorCommand.Make(target, this);
    }

    private class ColorCommand : BaseCommand
    {
        private ColorCommandAsset m_asset;

        private void Start()
        {
            bool didAddComponent = false;

            var component = GetComponent<EntityColorInfo>();
            if (component)
            {
               if (component.Color == m_asset.m_color)
               {
                   BaseCommand command = m_asset.m_command.Make(gameObject);

                   command.OnStatusChanged.AddListener(OnCommandStatusChanged);
                   
                   didAddComponent = true;
               } 
            }

            if (!didAddComponent)
            {
                Status = CommandStatus.COMPLETE;
            }
        }

        private void OnCommandStatusChanged(BaseCommand command)
        {
            command.enabled = false;
            command.OnStatusChanged.RemoveListener(OnCommandStatusChanged);

            Status = command.Status;
        }

        public static ColorCommand Make(GameObject owner, ColorCommandAsset asset)
        {
            var component = owner.AddComponent<ColorCommand>();
            component.m_asset = asset;
            return component;
        }
    }
}