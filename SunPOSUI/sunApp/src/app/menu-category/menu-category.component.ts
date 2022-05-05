import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs';
import * as fromModels from '../models';
import * as fromServices from '../services';
import { NgbModal, ModalDismissReasons, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-menu-category',
  templateUrl: './menu-category.component.html',
  styleUrls: ['./menu-category.component.scss']
})
export class MenuCategoryComponent implements OnInit {

  categories: fromModels.ICategory[] = [];
  menuItems: fromModels.IMenu[] = [];
  restaurant: fromModels.IRestaurant = {
    restaurantID: '',
    restaurantLocation: '',
    restaurantName: '',
    restaurantPhoneNumber: '',
    restaurantViewName: '',
    mondayHours: '',
    tuesdayHours: '',
    wednesdayHours: '',
    thursdayHours: '',
    fridayHours: '',
    saturdayHours: '',
    sundayHours: ''
  };
  
  menuItem: fromModels.IMenu = {
    menuId: 0,
    category: '',
    category2: '',
    item: '',
    item2: '',
    description: '',
    price: 0,
    lunchPrice: 0,
    dinnerPrice: 0,
    categoryID: '',
    categoryName: '',
    restaurantID: ''
  };

  result: fromModels.IResult = {
    message: '',
    isSuccessful: false
  };

  user: fromModels.IUser = {
    userID: '',
    userName: '',
    userFirstName: '',
    userLastName: ''
  }

  public closeResult = '';
  public restaurantId = '';
  public firstCategoryId = '';
  constructor(private sunposAPIService: fromServices.sunposAPIService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.sunposAPIService.getRestaurant().subscribe(result => {
      this.restaurant = result;

      this.restaurantId = this.restaurant.restaurantID;

      this.sunposAPIService.getCategories(this.restaurantId).subscribe(results => {
        this.categories = results;

        this.firstCategoryId = this.categories[0].categoryID;

        //this.clickedCategoryTab(this.firstCategoryId);
      });
    });

    this.sunposAPIService.getUser('ykevin98').subscribe(result => {
      this.user = result;
    })
  }

  clickedCategoryTab(categoryId: string): void {
    this.sunposAPIService.getMenuItems(categoryId).subscribe(results => {
      this.menuItems = results;
    });
  }

  open(content: any, item: fromModels.IMenu) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });

    this.menuItem = item;
  }

  addToCart(item: fromModels.IMenu): void {
    this.sunposAPIService.addToCart(item, this.user.userID, this.restaurantId).subscribe(result => {
      this.result = result;

      if (this.result.isSuccessful){
        this.modalService.dismissAll();
      }
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
