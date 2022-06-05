<template>
  <nav
    class="
      navbar navbar-expand-lg
      justify-content-between
      navbar-dark
      bg-success
      border-bottom border-info
      px-3
    "
  >
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div
        class="d-flex flex-column align-items-center"
        title="Go to Home Page"
      >
        <img
          class="logo rounded"
          alt="logo"
          src="../assets/img/keeprLogo.png"
          height="45"
        />
      </div>
    </router-link>
    <div>
      <Login />
    </div>
  </nav>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style scoped>
a:hover {
  text-decoration: none;
}
.logo {
  width: 2.5em;
}
.nav-link {
  text-transform: uppercase;
}
.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
