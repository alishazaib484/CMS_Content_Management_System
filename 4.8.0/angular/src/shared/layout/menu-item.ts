export class MenuItem {
    name = '';
    permissionName = '';
    icon = '';
    route = '';
    pageId='';
    isActive='';
    items: MenuItem[];

    constructor(name: string, permissionName: string, icon: string, route: string,pageId:string, childItems: MenuItem[] = null) {
        this.name = name;
        this.permissionName = permissionName;
        this.icon = icon;
        this.route = route;
        this.pageId=pageId;
        this.isActive="false";
        if (childItems) {
            this.items = childItems;
        } else {
            this.items = [];
        }
    }
}
