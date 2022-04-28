import { Component, OnInit } from '@angular/core';
import * as fromServices from '../services';
import * as fromModels from '../models';
import { IUser } from '../models';
import { UUID } from 'angular2-uuid';
@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})

export class AddUserComponent implements OnInit {

  user: fromModels.IUser = {
    userID: '',
    userName: '',
    userFirstName: '',
    userLastName: ''
  }
  result: fromModels.IResult = {
    message: '',
    isSuccessful: false
  }

  constructor(private sunposAPIService: fromServices.sunposAPIService) { }

  ngOnInit(): void {
  }

  addUser(): void{
    this.user.userID = UUID.UUID();

    this.sunposAPIService.addUser(this.user).subscribe(result =>{
      this.result = result;

      if (this.result.isSuccessful){
        
      }
    }) 
  }
}
