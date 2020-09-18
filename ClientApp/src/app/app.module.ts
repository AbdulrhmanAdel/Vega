import { AuthService } from './services/auth.service';
import { PhotoService } from './services/photo.service';
import { AppErrorHandler } from './app.error_handler';
import { ErrorHandler } from '@angular/core';
import { VehicleService } from './services/vehicle.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { PaginationComponent } from "./shared/pagination/pagination.component";
import { ViewVehicleComponent } from './view-vehicle/view-vehicle.component';
import { ChartComponent } from './chart/chart.component';
import { ChartModule } from "angular2-chartjs";
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    NotFoundComponent,
    VehiclesComponent,
    PaginationComponent,
    ViewVehicleComponent,
    ChartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ToastrModule.forRoot(),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '',redirectTo:'vehicles',  pathMatch: 'full' },
      {path:"vehicles/new" ,component:VehicleFormComponent},
      {path:"vehicles/edit/:id",component:VehicleFormComponent},
      {path:"vehicles/:id" ,component:ViewVehicleComponent},
      {path:"vehicles",component:VehiclesComponent},
      {path:"chart",component:ChartComponent},
      {path:"not-found",component:NotFoundComponent},
      {path:'home',component:HomeComponent},
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      {path:"test",component:PaginationComponent}
    ]),BrowserAnimationsModule,
    ChartModule
 
  ],
  providers: [VehicleService,
              {provide:ErrorHandler,useClass:AppErrorHandler},
              PhotoService,
              AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
