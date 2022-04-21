import { Component, OnInit } from '@angular/core';
import * as fromModels from '../models';
import * as fromServices from '../services';

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

    
  }

  clickedCategoryTab(categoryId: string): void {
    this.sunposAPIService.getMenuItems(categoryId).subscribe(results => {
      this.menuItems = results;
    })
  }
}
