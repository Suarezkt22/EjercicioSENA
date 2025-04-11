import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ModalTypes } from './modal.interface';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  standalone : true,
  imports: [CommonModule],
  styleUrls: ['./modal.component.css'],
})
export class ModalComponent {
  statusTitle: string = 'Información';
  @Input() message: string = 'Este es el mensaje del modal.';
  @Input() modalType!: ModalTypes; 
  @Output() close = new EventEmitter<void>();
  
  // Clases dinámicas según el tipo de modal
  get iconClass(): string {
    switch (this.modalType) {
      case ModalTypes.Error:
        return 'text-danger bi bi-exclamation-circle';
      case ModalTypes.Warning:
        return 'text-warning bi bi-exclamation-triangle';
      default:
        return 'text-info bi bi-info-circle';
    }
  }

  get textClass(): string {
    switch (this.modalType) {
      case ModalTypes.Error:
        this.statusTitle = "Error"
        return 'text-danger';
      case ModalTypes.Warning:
        this.statusTitle = "Warning"
        return 'text-warning';
      default:
        return 'text-info';
    }
  }

  get buttonClass(): string {
    switch (this.modalType) {
      case ModalTypes.Error:
        return 'btn-danger';
      case ModalTypes.Warning:
        return 'btn-warning';
      default:
        return 'text-info';
    }
  }

  closeModal() {
    this.close.emit();
  }
}
