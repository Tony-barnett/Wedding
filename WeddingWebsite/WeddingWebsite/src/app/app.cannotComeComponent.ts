import { Component, ViewChild } from '@angular/core';

import { Observable } from "rxjs/Observable";

import { GuestService } from "app/GuestService";
import { Guest } from "app/Guest";
import { Router } from "@angular/router";
import 'rxjs/add/operator/debounceTime';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/distinctUntilChanged';
import { AlertMessageComponent } from "app/app.alert";

@Component({
    selector: 'cannot-come',
    templateUrl: 'app.cannotComeComponent.html',
    styleUrls: ['app.guestForms.css']
})
export class CannotComeComponent {
    constructor(
        private guestService: GuestService
    ){}
    @ViewChild('alertMessage') alertMessage;
    child: string = "isChild";
    youngChild: string = "isYoungChild";
    baby: string = "isBaby";

    guests: Array<Guest>;
    
    //private setupEdit(hasChanged: Subject<Guest>): void {
    //    hasChanged
    //        .debounceTime(500)
    //        .subscribe(
    //        g => this.guestService
    //            .updateGuest(g)
    //            .subscribe(
    //            result => {
    //                if (result) {
    //                    this.guests[this.guests.indexOf(g)] = g;
    //                } else {
    //                    console.log("whert");
    //                }
    //            }));
    //}

    private hideAndShow(hideId: string, showId: string): void {
        document.getElementById(hideId).style.display = 'none';
        document.getElementById(showId).style.display = 'block';

    }

    ngOnInit(): void {
        this.guestService
            .getGuests(false)
            .subscribe(
                guest => this.guests = guest,
                error => console.log('bar', error)
            );

    };
    
    updateField(guest: Guest, field: string, hasChanged: Subject<Guest>, value: string): void {
        guest[field] = value.trim();
        hasChanged.next(guest);
        this.setSuccess(guest.firstName + " " + guest.surname + " was successfully updated.");
    };

    onDelete(guest: Guest): void {
        var buttonText = 'delete-text-' + guest.id;
        var spinner = 'delete-spin-' + guest.id;
        this.hideAndShow(buttonText, spinner);
        this.guestService
            .deleteGuest(guest)
            .subscribe(
            result => {
                this.hideAndShow(spinner, buttonText);
                this.guestWasDeleted(result, guest);
            },
            error => {
                this.hideAndShow(spinner, buttonText);
                console.log("couldn't do it cap'n", error)
            })
    };

    guestWasDeleted(yes: boolean, guest: Guest): void {
        if (yes) {
            var index = this.guests.indexOf(guest);
            this.guests.splice(index, 1);
            this.setSuccess(guest.firstName + " " + guest.surname + " was successfully removed.")
        }
    };

    addGuestToList(guest: Guest): void {
        this.guests.push(guest);
    };

    setSuccess(message: string): void {
        this.alertMessage.success(message);
    }
};