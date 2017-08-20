import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { AddedGuestsComponent } from "app/app.addedGuestComponent";
import { HttpModule } from "@angular/http";
import { GuestService } from "app/GuestService";
import { NewGuestComponent } from "app/app.makeGuestComponent";
import { AlertMessageComponent } from "app/app.alert";
import { ChooseGuestsComponent } from "app/app.rsvpChoiceComponent";

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from "@angular/router";
import { CannotComeComponent } from "app/app.cannotComeComponent";


const appRoutes: Routes = [
    {
        path:'canCome',
        component:AddedGuestsComponent
    },
    {
        path: 'cannotCome',
        component: CannotComeComponent
        }];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        RouterModule.forRoot(appRoutes)
    ],
    declarations: [
        AlertMessageComponent,
        AppComponent,
        AddedGuestsComponent,
        NewGuestComponent,
        ChooseGuestsComponent,
        CannotComeComponent
    ],
    providers: [
        AlertMessageComponent,
        GuestService,
        AddedGuestsComponent
    ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
