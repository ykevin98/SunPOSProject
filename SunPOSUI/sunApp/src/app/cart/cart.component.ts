import { ValueConverter } from '@angular/compiler/src/render3/view/template';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import * as fromModels from '../models';
import * as fromServices from '../services';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  cartItems: fromModels.ICart[] = [];
  user: fromModels.IUser = {
    userID: '',
    userName: '',
    userFirstName: '',
    userLastName: ''
  }

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

  result: fromModels.IResult = {
    message: '',
    isSuccessful: false
  };

  private restaurantId = '';
  private userId = '';
  public totalPrice = 0;
  private taxRate = environment.tax;
  public tax = 0;
  public subTotal = 0;

  constructor(private sunposAPIService: fromServices.sunposAPIService) { }

  ngOnInit(): void {
    this.sunposAPIService.getRestaurant().subscribe(result => {
      this.restaurant = result;

      this.restaurantId = this.restaurant.restaurantID;

      this.sunposAPIService.getUser('ykevin98').subscribe(result => {
        this.user = result;
  
        this.userId = this.user.userID;

        this.sunposAPIService.getCartItems(this.userId, this.restaurantId).subscribe(result => {
          this.cartItems = result;

          for (var i = 0; i < this.cartItems.length; i++){
            this.subTotal += this.cartItems[i].price;
          }

          this.tax = this.subTotal * this.taxRate;
          this.totalPrice = this.subTotal + this.tax;
        });
      });
    });
  }

  checkout(cartItems: fromModels.ICart[]) : void {
    this.sunposAPIService.checkout(cartItems).subscribe(result => {
      this.result = result;
    });
  }
}
