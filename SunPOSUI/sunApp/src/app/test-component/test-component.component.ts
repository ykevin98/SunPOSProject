import { Component, OnInit } from '@angular/core';
import * as fromServices from '../services';
import * as fromModels from '../models';


@Component({
  selector: 'app-test-component',
  templateUrl: './test-component.component.html',
  styleUrls: ['./test-component.component.scss']
})
export class TestComponentComponent implements OnInit {

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

  public restaurantId = '';
  constructor(private sunposAPIService: fromServices.sunposAPIService) { }

  ngOnInit(): void {
    this.sunposAPIService.getRestaurant().subscribe(result => {
      this.restaurant = result;

      this.restaurantId = this.restaurant.restaurantID;

      this.sunposAPIService.getCategories(this.restaurantId).subscribe(results => {
        this.categories = results;
      });
    });

    this.sunposAPIService.getMenuItems('CF6345CD-D79D-47B3-B4ED-2FC61D162F11').subscribe(results => {
      this.menuItems = results;
    })
  }

}
