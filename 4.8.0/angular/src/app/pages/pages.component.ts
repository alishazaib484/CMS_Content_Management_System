//import { Component, OnInit } from '@angular/core';
import { Component,ViewChild,OnInit,OnDestroy, Injector, NgZone} from '@angular/core';
import { Injectable } from '@angular/core';

import {
  CustomPageServiceProxy,
  CustomPageDto,
  GetAllPagesOutputDto,
  ListResultDtoOfGetAllPagesOutputDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
@Component({
  templateUrl: './pages.component.html',
  styleUrls: []
})

@Injectable({
  providedIn: 'root',
})
export class PagesComponent extends AppComponentBase implements OnInit, OnDestroy   {
  pages: GetAllPagesOutputDto[] = [];
  page:CustomPageDto;
  pagesss:CustomPageDto[]=[];
  title:any;
  static pageId:number;
  static lastComp: PagesComponent;

  constructor(
    injector: Injector,
    private _customPageService: CustomPageServiceProxy,
    private zone:NgZone,
    //private changeDetector: ChangeDetectorRef,
    //private _navBar:SideBarNavComponent
    
) {
  super(injector);
 
  this.page=new CustomPageDto();
  this.page.title="zxc";
  this.title="abc";
  this.pagesss.push(this.page);
   
    
}

name: string;
  ngOnInit(): void {
    if (!PagesComponent.lastComp) {
      this.name = 'Zion';
    }
  }

  ngOnDestroy(): void {
    PagesComponent.lastComp = this;
  }


  changeTitle():void{
    this.title="before";
    this.getPageContent(PagesComponent.pageId);
  }

  addNewPage(): void {
   
  }
  deletePage(): void {
    //this._router.navigate(['account/login']);
  }
  editPage(): void {
    
  }
  getPageContent(pageid): void {
    this._customPageService
    .getCMSContent(pageid)
    .subscribe((result: CustomPageDto) => {
    //   this.zone.run(() => { // <== added
    //     this.page.title="zxcff";
    //     this.title="abcdrfgg";
    // });
        this.page = result;
        this.title="wokkk";
        
    });
  }

  navigateToPage(item):void{
    //this.getPageContent(item.pageId);
    PagesComponent.pageId=item.pageId;
  }
  async getPagesList():Promise<ListResultDtoOfGetAllPagesOutputDto>{
 
   return this._customPageService
    .getAll().toPromise();

  }

  getAllPages():void{
    this._customPageService
    .getAll()
    .subscribe((result: ListResultDtoOfGetAllPagesOutputDto) => {
        this.pages = result.items;
        // for (let i = 0; i < this.pages.length; i++) {
        //   this._navBar.addMenuItem(this.pages[i].title,'','','/app/customPages');
        // }
    });
  }
}
