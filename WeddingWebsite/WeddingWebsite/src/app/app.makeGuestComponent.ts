import { Component, Input, Output, EventEmitter } from "@angular/core";

import { GuestService, NewGuestObject } from "app/GuestService";
import { Guest } from "app/Guest";
import { AddedGuestsComponent } from "app/app.addedGuestComponent";

@Component({
    selector: 'new-guest',
    templateUrl: 'app.makeGuestComponent.html'
})
export class NewGuestComponent {
    constructor(
        private guestService: GuestService,
        private guestTable: AddedGuestsComponent
    ) {
        this.guest = new Guest;
    }

    @Input() guest: Guest;
    @Input() justName: boolean;
    @Output() onAlert: EventEmitter<string> = new EventEmitter();
    @Output() onGuestAdded: EventEmitter<Guest> = new EventEmitter();

    private hideAndShow(hideId: string, showId: string): void {
        document.getElementById(hideId).style.display = 'none';
        document.getElementById(showId).style.display = 'block';

    }

    onSubmit() {
        this.hideAndShow('submit-text', 'spinny-span');

        this.guest.isComing = !this.justName;

        this.guestService
            .addGuest(this.guest)
            .subscribe(
            result => { this.hideAndShow('spinny-span', 'submit-text'); this.handleAddGuestPost(result) })
    };

    private handleAddGuestPost(result: NewGuestObject) {
        if (result.success == true) {
            this.guest.id = result.newId;
            this.updateTable();
        } else {
            this.errorOut();
        }

        this.guest = new Guest;
    };

    private updateTable(): void {
        this.onGuestAdded.emit(this.guest);
        this.onAlert.emit(this.guest.firstName + " " + this.guest.surname + " was successfully added.");
        //this.guestTable.guests.push(this.guest);
        //this.guestTable.alertMessage.success(this.guest.firstName + " " + this.guest.surname + " was successfully added.");
    };

    private errorOut(): void {
        console.log("shit gone sour");
        this.guestTable.alertMessage.error("Error, please try again.");
    };
};
