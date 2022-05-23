import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { takeUntil } from 'rxjs/operators';
import { UserService } from '../../services/user.service';
import { SnackbarService } from '../../services/snackbar.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.scss']
})
export class UserRegistrationComponent implements OnInit, OnDestroy {
  private unsubscribes$ = new Subject<void>();
  registerForm: FormGroup;
  submitted = false;
  formData = new FormData();
  files:any;
  coverImagePath:any;
  formTitle :string;
  userId:any

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute,
    private snackbarService: SnackbarService,
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<UserRegistrationComponent>) {}

  ngOnInit():void {
    this.coverImagePath= '/UserImage/' + 'Default_image.jpg';
    this.buildForm();
    this.selectTypeForm();
  }

  ngOnDestroy(): void {
    this.unsubscribes$.next();
    this.unsubscribes$.complete();
  }

  buildForm(): void {
    this.registerForm = this.formBuilder.group({
      userId: 0,
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      userName: ['', Validators.required],
      /*email: ['', [Validators.required, Validators.email]],*/
      gender: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      check:[false, Validators.requiredTrue],
      userImage:[''],
    }, {
      validator: this.MustMatch('password', 'confirmPassword')
    });
  }

  selectTypeForm() {
    if (this.route.snapshot.params['id']) {
      this.userId = this.route.snapshot.paramMap.get('id');
    }

    if (!this.userId) {
      this.formTitle ="Agregar";
      return 
    }

    this.formTitle = "Editar";
    this.userService.getUserById(this.userId)
      .pipe(takeUntil(this.unsubscribes$))
        .subscribe((result) => {
          this.setUserFormData(result);   
        }, error => {
          console.log('Error ocurred while fetching game data:', error);
        });
  }

  uploadImage(event) {
    this.files = event.target.files;
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = (myevent: ProgressEvent) => {
      this.coverImagePath = (myevent.target as FileReader).result;
    };
  }

  setUserFormData(userFormData) {
    this.registerForm.setValue({
      userId: userFormData.userId,
      firstName: userFormData.firstName,
      lastName: userFormData.lastName,
      userName: userFormData.userName,
      gender: userFormData.gender,
      password: "",
      confirmPassword: "",
      check:false,
      userImage:userFormData.userImage,
    });

    this.coverImagePath = '/UserImage/' + userFormData.userImage;
  }

  saveUserData(event: Event): void {
    // preventDefaultCancela comportamiento del html  que viene por defecto
    event.preventDefault();
    this.submitted = true;
    if (!this.registerForm.valid) {  
      return this.registerForm.markAllAsTouched(); 
    }
    if (this.files && this.files.length > 0) {
      for (let i = 0; i < this.files.length; i++) {
        this.formData.append('file' + i, this.files[i]);
      }
    } 
    this.formData.append('UserFormData', JSON.stringify(this.registerForm.value));     
    if (this.userId) {
      this.userService.updateUser(this.formData, this.userId)
        .pipe(takeUntil(this.unsubscribes$))
          .subscribe(() => {
            this.snackbarService.showSnackBar('Se ha editado con exito');  
            this.router.navigate(['/']);
          }, error => {
            console.log('Error ocurred while updating user data:', error);
          });
    } else {
      this.userService.addUser(this.formData)
        .pipe(takeUntil(this.unsubscribes$))
          .subscribe(() => {       
            this.snackbarService.showSnackBar('El usuario se ha registrado con exito');
            this.router.navigate(['/']);
          }, error => {
            this.registerForm.controls['userName'].setErrors({ 'incorrect': true});         
            this.formData.delete('UserFormData');
            console.log('Error ocurred while user register: ', error);                  
          });
      }
  }

  private MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
        // returna si otro validador ya ha encontrado un error en el matchingControl
        return;
      }
        // establecer error en matchingControl si falla la validación
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  }

  get firstNameField(): AbstractControl {
    return this.registerForm.get('firstName');
  }
  get lastname(): AbstractControl {
    return this.registerForm.get('lastName');
  }
  get username(): AbstractControl {
    return this.registerForm.get('userName');
  }
  get password(): AbstractControl {
    return this.registerForm.get('password');
  }
  get gender(): AbstractControl {
    return this.registerForm.get('gender');
  }
  get check(): AbstractControl {
    return this.registerForm.get('check');
  }
  get confirmPassword(): AbstractControl {
    return this.registerForm.get('confirmPassword');
  }
  get f() { return this.registerForm.controls; }
  get userImage() {
    return this.registerForm.get('userImage');
  }
}
