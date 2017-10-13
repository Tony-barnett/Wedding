import { Component } from "@angular/core";
import { animate, state, style, transition, trigger } from "@angular/animations";

import UIkit from 'uikit'

@Component({
    selector: "alert-message",
    templateUrl: "app.alert.html",
    animations: [

        trigger("thingState", [
            transition("* => void",

                animate('0.2s 0.1s ease-out', style({
                    opacity: 0,
                    transform: 'translateY(-100%)'
                })))
    ])]
})
export class AlertMessageComponent {
    alertType: string = "alert-info";
    alertMessage: string = null;
    alerts: Array<Alert>;

    constructor() {
        this.alerts = new Array<Alert>();
    }

    showMessage(type, message): void {
        UIkit.notification({
            message: message,
            status: type,
            pos: 'top-center',
            timeout: 10000
        });

    }
    error(message: string): void {
        this.showMessage("danger", message);
    }

    success(message: string): void {
        this.showMessage("success", message);
    }
}

export class Alert {
    type: string;
    message: string;
}