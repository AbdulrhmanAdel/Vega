import { KeyValuePair } from './../models/keyValuePair';
import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import {  Vehicle }  from "./../models/vehicle";
import { query } from '@angular/animations';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent implements OnInit {
  private readonly pageSize= 3;
  models:KeyValuePair[];
  makes:any[];
  query:any ={
    
  };
  queryResult:any={};

  columns=[
    
    {title:'Make' ,key:"make",isSortable:false},
    {title:'Model' ,key:"model",isSortable:false},
    {title:'Contact Name' ,key:"contactName", isSortable:false},
    {}
  ]

  constructor(
    private vehicleService:VehicleService
  ) { }

  ngOnInit() {

    this.populateVehicles();
    this.vehicleService.getMakes().subscribe(makes =>{this.makes =makes as KeyValuePair[];});
  }

  private populateVehicles(){
    this.vehicleService.getVehicles(this.query).subscribe(result =>{
      console.log(result)
      this.queryResult=result as any[];
      console.log(this.query)
    });

  }
  onFilterChange(){
    this.query.modelId = null;
    var selectedMake = this.makes.find(m => m.id == this.query.makeId);
    this.models =selectedMake ? selectedMake.models:[];
    this.populateVehicles();
  }
  onModelChange(){
    this.populateVehicles();
  }

  onPageChange(page){
    this.query.page =page;
    this.populateVehicles();
  }

  reset(){
    // this.query={
    //   page:1,
    //   pageSize:this.pageSize
    // }; 
    this.query={};
    this.populateVehicles();
  
  }

 
    
  search(){
   this.populateVehicles();

  }

  sortBy(coulmnName){
    this.columns.find(k => k.key == coulmnName as string).isSortable = true;
    

    if(this.query.sortBy === coulmnName){
      this.query.isSortAscending = !this.query.isSortAscending ;
    }else{
      this.query.sortBy = coulmnName;
      this.query.isSortAscending = true ;

    }
    
    this.populateVehicles();
  }

}
