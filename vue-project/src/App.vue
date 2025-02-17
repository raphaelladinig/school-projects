<template>
  <div class="user-profile">
    <img
      :src="selectedAvatar || defaultImage"
      @error="handleImageError"
      class="avatar"
      alt="Benutzeravatar"
    >

    <div class="form-group">
      <label>Profilbild auswählen:</label>
      <select
        v-model="selectedAvatar"
        class="image-select"
        @change="updateAvatar"
      >
        <option value="">Standardbild verwenden</option>
        <option
          v-for="(avatar, index) in avatars"
          :key="index"
          :value="avatar.url"
          :style="styleOption(avatar)"
        >
          {{ avatar.name }}
        </option>
      </select>
    </div>

    <div class="form-group">
      <label>Benutzername:</label>
      <input
        type="text"
        v-model="username"
        placeholder="Max Mustermann"
        class="name-input"
      >
    </div>

    <button
      class="submit-btn"
      @click="submitForm"
      :class="{ 'animate': isAnimating }"
      @animationend="isAnimating = false"
    >
      {{ buttonText }}
    </button>
  </div>
</template>

<script>
export default {
  data() {
    return {
      username: '',
      selectedAvatar: '',
      avatars: [
        {
          name: 'Natur-Hintergrund',
          url: '/images/avatars/nature.jpg',
          thumb: '/images/avatars/thumbs/nature-thumb.jpg'
        },
        {
          name: 'Abstraktes Design',
          url: '/images/avatars/abstract.png',
          thumb: '/images/avatars/thumbs/abstract-thumb.png'
        }
      ],
      defaultImage: '/images/default-avatar.png',
      isAnimating: false,
      buttonText: 'Profil speichern'
    }
  },
  methods: {
    handleImageError() {
      this.selectedAvatar = this.defaultImage
    },
    styleOption(avatar) {
      return {
        backgroundImage: `url(${avatar.thumb})`,
        backgroundSize: '20px 20px',
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'left center',
        paddingLeft: '30px'
      }
    },
    submitForm() {
      if (!this.username) {
        alert('Bitte Namen eingeben!')
        return
      }

      this.isAnimating = true
      this.buttonText = '🔄 Wird gespeichert...'

      // Simulierte API-Anfrage
      setTimeout(() => {
        this.buttonText = '✅ Gespeichert!'
        setTimeout(() => this.buttonText = 'Profil speichern', 2000)
      }, 1000)
    },
    updateAvatar() {
      if (!this.selectedAvatar) {
        this.selectedAvatar = this.defaultImage
      }
    }
  }
}
</script>

<style scoped>
.user-profile {
  max-width: 400px;
  margin: 2rem auto;
  padding: 20px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  border-radius: 8px;
}

.avatar {
  display: block;
  width: 150px;
  height: 150px;
  border-radius: 50%;
  margin: 0 auto 1.5rem;
  object-fit: cover;
  border: 3px solid #eee;
}

.form-group {
  margin-bottom: 1.5rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  color: #333;
  font-weight: 500;
}

.image-select {
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  background-color: #fff;
}

.image-select option {
  padding: 8px 12px 8px 35px;
  background-repeat: no-repeat;
  background-position: 8px center;
}

.name-input {
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.submit-btn {
  display: block;
  width: 100%;
  padding: 12px;
  background: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.submit-btn:hover {
  background: #0056b3;
  transform: translateY(-1px);
}

.animate {
  animation: bounce 0.5s;
}

@keyframes bounce {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-5px); }
}
</style>

