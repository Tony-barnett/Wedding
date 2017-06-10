import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { AddedGuestsComponent } from "app/app.addedGuestComponent";
import { HttpModule } from "@angular/http";
import { GuestService } from "app/GuestService";
import { NewGuestComponent } from "app/app.makeGuestComponent";

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule
    ],
    declarations: [
        AppComponent,
        AddedGuestsComponent,
        NewGuestComponent
    ],
    providers: [
        GuestService,
        AddedGuestsComponent
    ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
