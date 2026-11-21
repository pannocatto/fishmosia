using Content.Shared.Actions;
using Content.Shared.Popups;

namespace Content.Shared.Carpmosia.Abilities;
public abstract partial class SharedCrawlUnderObjectsSystem : EntitySystem
{
    [Dependency] private SharedActionsSystem _actions = default!;
    [Dependency] private SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CrawlUnderObjectsComponent, CrawlingUpdatedEvent>(OnCrawlingUpdated);
        SubscribeLocalEvent<CrawlUnderObjectsComponent, MapInitEvent>(OnMapInit);
    }

	private void OnMapInit(Entity<CrawlUnderObjectsComponent> ent, ref MapInitEvent args)
    {
        if (ent.Comp.ToggleHideAction != null)
            return;

        _actions.AddAction(ent, ref ent.Comp.ToggleHideAction, ent.Comp.ActionProto);
    }

    private void OnCrawlingUpdated(EntityUid uid,
        CrawlUnderObjectsComponent component,
        CrawlingUpdatedEvent args)
    {
        if (args.Enabled)
            _popup.PopupEntity(Loc.GetString("crawl-under-objects-toggle-on"), uid);
        else
            _popup.PopupEntity(Loc.GetString("crawl-under-objects-toggle-off"), uid);
    }
}
