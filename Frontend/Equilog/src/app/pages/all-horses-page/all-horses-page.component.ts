import { Component, inject, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HorsesService } from '../../services/horse.service';
import { FormsModule } from '@angular/forms';
import { HorseDto } from '../../domain/client';
import { FooterComponent } from '../../components/footer/footer.component';
import { HeaderComponent } from '../../components/header/header.component';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-all-horses-page',
  imports: [CommonModule, FormsModule, FooterComponent, HeaderComponent],
  templateUrl: './all-horses-page.component.html',
  styleUrls: ['./all-horses-page.component.scss'],
})
export class AllHorsesPageComponent implements OnInit, OnDestroy {
  private horsesService = inject(HorsesService);
  private destroy$ = new Subject<void>();

  allHorses: HorseDto[] = [];
  loading = true;
  error: string | null = null;

  ngOnInit(): void {
    this.loadAllHorses();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadAllHorses(): void {
    this.loading = true;
    this.error = null;

    this.horsesService
      .getAllHorses()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: horses => {
          this.allHorses = horses;
          this.loading = false;
        },
        error: error => {
          console.error('Error loading all horses: ', error);
          this.error = 'Kunde inte hämta hästar. Försök igen senare.';
          this.loading = false;
        },
      });
  }

  trackByHorseId(index: number, horse: HorseDto): unknown {
    return horse.id || index;
  }

  onImageError(event: Event): void {
    const target = event.target as HTMLImageElement;
    target.src = '/default-horse.png';
  }
}
