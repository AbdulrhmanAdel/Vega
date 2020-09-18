import { KeyValuePair } from './../models/keyValuePair';
import { Vehicle } from './../models/vehicle';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute,Router } from "@angular/router";
import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { forkJoin } from "rxjs";
import { SaveVehicle } from '../models/saveVehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  
  vehicle:SaveVehicle={
    id:0, 
    makeId:0,
    modelId:0,
    isRegistered:false,
    features:[],
    contact:{
      name:"",
      phone:"",
      email:""
    }
  };
  
  modelId:number=this.vehicle.modelId;
  models: KeyValuePair[];
  makes: any[];
  features:any[];
  

  constructor(
    private vehicleService:VehicleService,
    private toastr:ToastrService,
    private route:ActivatedRoute,
    private router:Router
     ) {
      this.vehicle.id = route.snapshot.paramMap.get("id") as unknown as number;

     }

  ngOnInit() {
    let sources =[
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures()
    ];
    if(this.vehicle.id)
      sources.push(this.vehicleService.getVehicle(this.vehicle.id))
        
    forkJoin(sources).subscribe(data => {
      this.makes = data[0] as any[];
      this.features = data[1] as any [];
      
     
      if(this.vehicle.id)
      {
        this.setVehicle(data[2] as Vehicle);
        this.populateModels();
      }
      
    },err => {
      this.router.navigate(["/not-found"]);

    });
    
  
  }
  private setVehicle(v:Vehicle){
    this.vehicle.id =v.id;
    this.vehicle.makeId=v.make.id;
    this.vehicle.modelId=v.model.id;
    v.features.forEach(element => {
      this.vehicle.features.push(element.id);
    });
    this.vehicle.contact=v.contact;
  }

  private populateModels(){
    var selectedMake =this.makes.find(m => m.id == this.vehicle.makeId);
   console.log(selectedMake)

    this.models = selectedMake ? selectedMake.models :[];
  }
  onMakeChange(){
    
    this.populateModels();
   
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId,$event){
    if($event.target.checked)
        this.vehicle.features.push(featureId as never);
    else{
     let index = this.vehicle.features.indexOf(featureId as never);
     this.vehicle.features.splice(index,1);
    }

  }
  submit(){
    var result =(this.vehicle.id !=0 || this.vehicle.id != null) ? this.vehicleService.update(this.vehicle) : this.vehicleService.create(this.vehicle);
    if(this.vehicle.id == null){
      this.vehicle.id=Number(this.vehicle.id);
    }else{
      this.vehicle.id=Number(this.vehicle.id);
    }
      this.vehicle.modelId =Number(this.vehicle.modelId);
      result.subscribe(vehicle => {
      this.toastr.success("Data was successfully saved.","success",{timeOut:5000});
      this.router.navigate(["/vehicles"])
    })
  }
  
 
}
