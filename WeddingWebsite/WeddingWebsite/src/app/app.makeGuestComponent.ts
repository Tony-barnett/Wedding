import { Component, Input } from "@angular/core";

import { GuestService } from "app/GuestService";
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

    onSubmit() {
        this.guestService
            .addGuest(this.guest)
            .subscribe(
            result => this.handleAddGuestPost(result))
    };

    private handleAddGuestPost(result: boolean) {
        if (result == true) {
            this.updateTable()
        } else {
            this.errorOut();
        }

        this.guest = new Guest;
    };

    private updateTable(): void {
        this.guestTable.guests.push(this.guest);
    };

    private errorOut(): void {
        console.log("shit gone sour");
    };
};
