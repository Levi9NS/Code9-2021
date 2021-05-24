import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

constructor(private toastr: ToastrService) { }

errorMessage(message) {
  this.toastr.error(message, '', { timeOut: 0, positionClass: 'toast-bottom-right' });
}

warningMessage(message) {
  this.toastr.warning(message, '', { positionClass: 'toast-bottom-right' });
}

infoMessage(message) {
  this.toastr.info(message, '', { positionClass: 'toast-bottom-right'});
}

successMessage(message) {
  this.toastr.success(message, '', { positionClass: 'toast-bottom-right'});
}

}