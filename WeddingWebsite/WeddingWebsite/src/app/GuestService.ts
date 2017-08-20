
import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';

import { Guest } from "app/Guest";

export class NewGuestObject {
    success: boolean;
    newId: AAGUID;
}

@Injectable()
export class GuestService {
    constructor(private http: Http) { }

    getGuests(isComing: boolean): Observable<Guest[]> {
        return this.http
            .get("/RSVP/GetStoredGuests?isComing=" + isComing)
            .map(this.extract);
    }

    addGuest(guest: Guest): Observable<NewGuestObject> {
        this.tidyUpAge(guest);
        return this.http
            .post("/RSVP/AddGuest", guest)
            .map(response => {
                guest.id == response.json().guestId;
                var ngo = new NewGuestObject();
                ngo.newId = response.json().guestId;
                ngo.success = response.json().result == "Success";

                return  ngo;
            });
    }

    deleteGuest(guest: Guest): Observable<boolean> {
        return this.http
            .delete("/RSVP/RemoveGuest?guestId=" + guest.id)
            .map(response => response.json() == "Success");
    }

    updateGuest(guest: Guest): Observable<boolean> {
        this.tidyUpAge(guest);
        return this.http
            .put("/RSVP/EditGuest", guest)
            .map(response => response.json() == "Success");
    }

    private extract(response: Response) {
        let body = response.json();
        return body || {};
    }

    private tidyUpAge(guest: Guest) {
        guest.isChild = guest.isChild && !guest.isYoungChild && !guest.isBaby;
        guest.isYoungChild = guest.isYoungChild && !guest.isBaby;
    }
}