import { Component, Injector,NgZone } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import {
  CustomPageServiceProxy,
  CustomPageDto,
  GetAllPagesOutputDto,
  ListResultDtoOfGetAllPagesOutputDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { Router } from '@angular/router';

@Component({
  selector: 'app-custom-pages',
  templateUrl: './custom-pages.component.html',
  styleUrls: ['./custom-pages.component.css']
})

export class CustomPagesComponent extends AppComponentBase   {
  pages: GetAllPagesOutputDto[] = [];
  static page:CustomPageDto=new CustomPageDto();
  static isResponsePending:any=false;
  static canAddPage:any=false;
  static canEditPage:any=false;
  static canDeletePage:any=false;
  static pageState:string="add";
  static pageAdded: BehaviorSubject<any>=new BehaviorSubject<any>(null);

  constructor(
    injector: Injector,
    private _customPageService: CustomPageServiceProxy,
    private zone:NgZone,
    private _router:Router,
  
    
) {
  super(injector);
  // checking permissions
  CustomPagesComponent.canAddPage=this.permission.isGranted("Pages.AddPage");
  CustomPagesComponent.canEditPage=this.permission.isGranted("Pages.EditPage");
  CustomPagesComponent.canDeletePage=this.permission.isGranted("Pages.DeletePage");
    
}

//getter and setters 
  get page(){
    return CustomPagesComponent.page;
  }
  set page(page){
    CustomPagesComponent.page=page;
  }
  get canAddPage(){
    return CustomPagesComponent.canAddPage;
  }

  get canEditPage(){
   
    return CustomPagesComponent.canEditPage;
  }
  get canDeletePage(){
    return CustomPagesComponent.canDeletePage;
  }

  get pageState(){
    return CustomPagesComponent.pageState;
  }

  get isResponsePending(){
    return CustomPagesComponent.isResponsePending;
  }

  //this function add or update page  and then call for updating sideBarMenu accordingly
  //display message on screen according to action performed
  addOrUpdatePage(): void {
    if(CustomPagesComponent.page.title!=null && CustomPagesComponent.page.title==""){
      this.notify.error(this.l("Page Title is required"));
      return;
    }
   
    CustomPagesComponent.isResponsePending=true;
    this._customPageService.insertOrUpdateCMSContent(CustomPagesComponent.page)
    .subscribe((result: string) => {
      CustomPagesComponent.isResponsePending=false;
      if( /^\d+$/.test(result)){
        if(CustomPagesComponent.pageState=="edit"){ // page is updated
          CustomPagesComponent.pageState="view";
          const menuItem = {
            name:CustomPagesComponent.page.title,
            permission:'',
            icon:'playlist_play',
            route:'/app/customPages',
            pageId:CustomPagesComponent.page.id,
            type:"Updated"
          }
          CustomPagesComponent.pageAdded.next(menuItem);
          this.notify.info(this.l("Page Updated Successfully"));
        }
        else{ // new page is added 
        
          const menuItem = {
            name:CustomPagesComponent.page.title,
            permission:'',
            icon:'playlist_play',
            route:'/app/customPages',
            pageId:result.toString(),
            type:"Added"
          }
          CustomPagesComponent.pageAdded.next(menuItem);
          CustomPagesComponent.page=new CustomPageDto();
          this.navigateToPage(menuItem);
          this.notify.info(this.l("Page Added Successfully"));
        } 
      }
      else{
        this.notify.info(this.l(result));

      }     
    });
  }

  //delete page and delete item from menu too
  deletePage(): void {
    abp.message.confirm(
      this.l('', ''),
      (result: boolean) => {
          if (result) {
            CustomPagesComponent.isResponsePending=true;
            this._customPageService
            .deleteCMSContent(CustomPagesComponent.page.id)
            .subscribe((result: string) => {
              CustomPagesComponent.isResponsePending=false;
              const menuItem = {
                name:CustomPagesComponent.page.title,
                permission:'',
                icon:'playlist_play',
                route:'/app/customPages',
                pageId:CustomPagesComponent.page.id.toString(),
                type:"Deleted"
              }
              CustomPagesComponent.pageAdded.next(menuItem);
              CustomPagesComponent.page=new CustomPageDto();
              this.notify.info(this.l("Page Deleted Successfully"));
            });
          }
      }
  );
    
  }

  // change state of page to edit,content of page can only be updated in edit state oterwise it is disabled
  editPage(): void {
    CustomPagesComponent.pageState="edit";
  }

  // to get content of any specific page base on id
  getPageContent(pageid): void {
    CustomPagesComponent.isResponsePending=true;
    this._customPageService
    .getCMSContent(pageid)
    .subscribe((result: CustomPageDto) => {
      CustomPagesComponent.isResponsePending=false;
      CustomPagesComponent.page = result;
        
    });
  }

  //this is used to navigate to particular page
  //called from SideNavBarComponent too when menu item is selected which is related to custom page
  navigateToPage(item):void{
    this._router.navigate(['/app/customPages']);
    CustomPagesComponent.page=new CustomPageDto();
    CustomPagesComponent.page.title="";
    if(item.pageId>0){
      this.getPageContent(item.pageId);
      CustomPagesComponent.pageState="view";
    }else{
      CustomPagesComponent.pageState="add"
    } 
  }


  //function to get list of all pages
  // wait untill it get response from server then return list of pages
  async getPagesList():Promise<ListResultDtoOfGetAllPagesOutputDto>{
 
   return this._customPageService
    .getAll().toPromise();

  }

  //function to get list of all pages
  getAllPages():void{
    this._customPageService
    .getAll()
    .subscribe((result: ListResultDtoOfGetAllPagesOutputDto) => {
        this.pages = result.items;
       
    });
  }
}
