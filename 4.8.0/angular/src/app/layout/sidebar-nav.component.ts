import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';
import {CustomPagesComponent} from '@app/custom-pages/custom-pages.component';
import { BehaviorSubject } from 'rxjs';

import { 
    GetAllPagesOutputDto,
 
  } from '@shared/service-proxies/service-proxies';
@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})

export class SideBarNavComponent extends AppComponentBase{

    my_value: BehaviorSubject<string>;
    subscription:any;
    menuItems: MenuItem[] = [
        new MenuItem('HomePage', '', 'home', '/app/home',''),

        new MenuItem(this.l('Tenants'), 'Pages.Tenants', 'business', '/app/tenants',''),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/app/users',''),
        new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/app/roles',''),
        new MenuItem(this.l('About'), '', 'info', '/app/about',''),
        new MenuItem(this.l("Add Page"), 'Pages.AddPage', 'playlist_add', '/app/customPages','0'),
       // new MenuItem(this.l("Test Page"), '', 'local_offer', '/app/pages',''),


        //new MenuItem(this.l('helloooo'), '', 'info', '/app/about',''),

        new MenuItem(this.l('MultiLevelMenu'), '', 'menu', '','', [
            new MenuItem('ASP.NET Boilerplate', '', '', '','', [
                new MenuItem('Home', '', '', 'https://aspnetboilerplate.com/?ref=abptmpl',''),
                new MenuItem('Templates', '', '', 'https://aspnetboilerplate.com/Templates?ref=abptmpl',''),
                new MenuItem('Samples', '', '', 'https://aspnetboilerplate.com/Samples?ref=abptmpl',''),
                new MenuItem('Documents', '', '', 'https://aspnetboilerplate.com/Pages/Documents?ref=abptmpl','')
            ]),
            new MenuItem('ASP.NET Zero', '', '', '','', [
                new MenuItem('Home', '', '', 'https://aspnetzero.com?ref=abptmpl',''),
                new MenuItem('Description', '', '', 'https://aspnetzero.com/?ref=abptmpl#description',''),
                new MenuItem('Features', '', '', 'https://aspnetzero.com/?ref=abptmpl#features',''),
                new MenuItem('Pricing', '', '', 'https://aspnetzero.com/?ref=abptmpl#pricing',''),
                new MenuItem('Faq', '', '', 'https://aspnetzero.com/Faq?ref=abptmpl',''),
                new MenuItem('Documents', '', '', 'https://aspnetzero.com/Documents?ref=abptmpl','')
            ])
        ])
    ];

    constructor(
        injector: Injector,
        private _customPages:CustomPagesComponent,
       
    ) {
        super(injector);
        this.getMenuItemsToAdd();
        this.menuItems[0].isActive='true';
       
        
    }
    
    ngOnInit() {
        CustomPagesComponent.pageAdded.subscribe((my_value)=>this.newPageAdded(my_value));

    }

    newPageAdded(item: any) {
        if(item!=null && item.pageId>0){
            if(item.type=="Added"){
                this.addMenuItem(item.name,item.permission,item.icon,'/app/customPages',item.pageId);
                this.activate(this.menuItems.length-3);
            }
            else if(item.type=="Deleted"){            
                this.deleteMenuItem(item);     
            }else{
                this.updateMenuItem(item);     
            }
           
                
        }

    }


    //update SideBarMenu when new page is addedby user
    updateMenuItem(item:any):void{
        for(let i=0;i<this.menuItems.length;i++){
            if(this.menuItems[i].pageId==item.pageId){
                this.menuItems[i].name=item.name;
            }
        }
           
    }

    //delete item form SideBarMenu and redirect to Add Page 
    deleteMenuItem(item:any):void{
        for(let i=0;i<this.menuItems.length;i++){
            if(this.menuItems[i].pageId==item.pageId){
                this.menuItems.splice(i,1);
                break;
            }
        }

    this._customPages.navigateToPage(this.menuItems[this.menuItems.length-1]);
    this.activate(this.menuItems.length-2);
    }



    //this function get list of pages and add them on SideBArMenu 
    async getMenuItemsToAdd():Promise<void>{
        var response=await this._customPages.getPagesList();
        var pages=response.items;
        for (let i = 0; i < pages.length; i++) {
          this.addMenuItem(pages[i].title,'','playlist_play','/app/customPages',pages[i].id.toString());
        }
    }

    activate(index):void{
        for(let i=0;i<this.menuItems.length;i++){
            this.menuItems[i].isActive="false";
        }
        this.menuItems[index].isActive="true";
    }

    itemClicked(MenuItem):void{
      
       var a=MenuItem
        this._customPages.navigateToPage(MenuItem);
    }
    addMenuItem(name,permission,icon,route,pageid):void{
        this.menuItems.splice(this.menuItems.length-2, 0, new MenuItem(name, permission, icon, route,pageid));
        //this.menuItems.push(new MenuItem(this.l(name), permission, icon, route))
        
    }

    // check if user has acces to this page
    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}
