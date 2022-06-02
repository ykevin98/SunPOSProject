import { Component, OnInit } from '@angular/core';
import * as fromServices from '../../services';
import * as fromModels from '../../models';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';
import { faEdit } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  
  shoppingCart = faShoppingCart;
  editIcon = faEdit;
  cartItems: fromModels.ICart[] = [];
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

  user: fromModels.IUser = {
    userID: '',
    userName: '',
    userFirstName: '',
    userLastName: ''
  }

  public fullName = '';
  public userName = '';
  public cartItemCount = 0;
  public restaurantId = '';
  public userId = '';

  constructor(private sunposAPIService: fromServices.sunposAPIService) { }

  ngOnInit(): void {
    this.sunposAPIService.getRestaurant().subscribe(result => {
      this.restaurant = result;

      this.restaurantId = this.restaurant.restaurantID;

      this.sunposAPIService.getUser('ykevin98').subscribe(result =>{
        this.user = result;
  
        this.fullName = this.user.userFirstName + ' ' + this.user.userLastName;
        this.userName = this.user.userName;
  
        this.userId = this.user.userID;

        this.sunposAPIService.getCartItems(this.userId, this.restaurantId).subscribe(result =>{
          this.cartItemCount = result.length;
        });
      });
    });
  }
}
