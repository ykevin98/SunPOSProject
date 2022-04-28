import { Component, OnInit } from '@angular/core';
import * as fromServices from '../../services';
import * as fromModels from '../../models';
import { faShoppingCart } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  
  shoppingCart = faShoppingCart;
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

  constructor(private sunposAPIService: fromServices.sunposAPIService) { }

  ngOnInit(): void {
    this.sunposAPIService.getRestaurant().subscribe(result => {
      this.restaurant = result;
    });
  }

}
