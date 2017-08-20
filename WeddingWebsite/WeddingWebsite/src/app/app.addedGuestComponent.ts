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
    selector: 'added-guests',
    templateUrl: 'app.addedGuestComponent.html'
})
export class AddedGuestsComponent {
    constructor(
        private guestService: GuestService//,
        //private alertMessage: AlertMessageComponent
    ){}
    @ViewChild('alertMessage') alertMessage;
    child: string = "isChild";
    youngChild: string = "isYoungChild";
    baby: string = "isBaby";

    guests: Array<Guest>;

    private firstNameChanged = new Subject<Guest>();
    private surnameChanged = new Subject<Guest>();
    private allergiesChanged = new Subject<Guest>();


    private setupEdit(hasChanged: Subject<Guest>): void {
        hasChanged
            .debounceTime(500)
            .subscribe(
            g => this.guestService
                .updateGuest(g)
                .subscribe(
                result => {
                    if (result) {
                        this.guests[this.guests.indexOf(g)] = g;
                    } else {
                        console.log("whert");
                    }
                }));
    }

    private hideAndShow(hideId: string, showId: string): void {
        document.getElementById(hideId).style.display = 'none';
        document.getElementById(showId).style.display = 'block';

    }

    ngOnInit(): void {
        this.guestService
            .getGuests(true)
            .subscribe(
                guest => this.guests = guest,
                error => console.log('bar', error)
            );
        this.setupEdit(this.firstNameChanged);
        this.setupEdit(this.surnameChanged);
        this.setupEdit(this.allergiesChanged);
    };
    
    updateField(guest: Guest, field: string, hasChanged: Subject<Guest>, value: string): void {
        guest[field] = value.trim();
        hasChanged.next(guest);
        this.alertMessage.success(guest.firstName + " " + guest.surname + " was successfully updated.");
    };

    updateFirstName(guest: Guest, value: string): void {
        if (value != null && !value.match(/^ *$/)) {
            this.updateField(guest, "firstName", this.firstNameChanged, value);
        }
    };

    updateSurname(guest: Guest, value: string): void {
        if (value != null && !value.match(/^ *$/)) {
            this.updateField(guest, "surname", this.surnameChanged, value);
        }
    };

    updateAllergies(guest: Guest, value: string): void {
        this.updateField(guest, "allergies", this.allergiesChanged, value);
    };

    updateAge(guest: Guest, ageGroup: string): void {
        switch (ageGroup) {
            case this.child:
                guest.isChild = !guest.isChild;
                guest.isYoungChild = false;
                guest.isBaby = false;
                break;
            case this.youngChild:
                guest.isChild = false;
                guest.isYoungChild = !guest.isYoungChild;
                guest.isBaby = false;
                break;
            case this.baby:
                guest.isChild = false;
                guest.isYoungChild = false;
                guest.isBaby = !guest.isBaby;
                break;
        }

        this.firstNameChanged.next(guest);
    }

    showAndHide(showId: string, hideId: string, guestId: AAGUID, focusId: string): void{
        document.getElementById(hideId + "-" + guestId).style.display = "none";
        document.getElementById(showId + "-" + guestId).style.display = "block";
        if (focusId) {
            document.getElementById(focusId + "-" + guestId).focus();
        }
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
            this.alertMessage.success(guest.firstName + " " + guest.surname + " was successfully removed.")
        }
    };

    addGuestToList(guest: Guest): void {
        this.guests.push(guest);
    };

    setSuccess(message: string): void {
        this.alertMessage.success(message);
    }
};